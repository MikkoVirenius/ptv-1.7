﻿/**
 * The MIT License
 * Copyright (c) 2016 Population Register Centre (VRK)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Hosting;
using PTV.Framework.Interfaces;
using PTV.Framework.ServiceManager;

namespace PTV.Framework
{
    public static class CoreExtensions
    {
        /// <summary>
        /// Call action only on object that is not null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="action">Action that will be called on target object</param>
        public static void SafeCall<T>(this T target, Action<T> action) where T:  class
        {
            if (target == null) return;
            action(target);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="target"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static TOut SafeCall<T, TOut>(this T target, Func<T, TOut> action) where T : class
        {
            if (target == null) return default(TOut);
            return action(target);
        }


        /// <summary>
        /// Get property of object from collection if this object is not null
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="enumerable">Collection that is searched</param>
        /// <param name="property">Value of this property will be returned</param>
        /// <returns>Value of property</returns>
        public static TResult SafePropertyFromFirst<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> property)
        {
            if (enumerable == null) return default(TResult);
            var instance = enumerable.FirstOrDefault();
            if (instance == null) return default(TResult);
            return property(instance);
        }

        /// <summary>
        /// Set value to property of target object
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <typeparam name="TOutProperty"></typeparam>
        /// <param name="target">Target object which property will be set</param>
        /// <param name="memberLamda">Property selector</param>
        /// <param name="value">Value that will be set to property</param>
        public static void SetPropertyValue<TTarget, TOutProperty>(this TTarget target, Expression<Func<TTarget, TOutProperty>> memberLamda, object value)
        {
            var memberSelectorExpression = memberLamda.Body as MemberExpression ?? ((UnaryExpression)memberLamda.Body).Operand as MemberExpression;

            if (memberSelectorExpression == null)
            {
                throw new Exception("Property selector cast error.");
            }

            var property = memberSelectorExpression.Member as PropertyInfo;
            if (property == null)
            {
                throw new Exception("Memberexpression cast error.");
            }

            property?.SetValue(target, value, null);
        }
       
        /// <summary>
        /// Add items to list from another list if they are not already present
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="addingList"></param>
        /// <returns></returns>
        public static List<T> AddWithDistinct<T>(this List<T> list, IEnumerable<T> addingList)
        {
            list.AddRange(addingList.Except(list));
            return list;
        }

        /// <summary>
        /// Creates lambda expression for equality from property name and value
        /// </summary>
        /// <typeparam name="T">Type on which the property will be used</typeparam>
        /// <param name="propertyName">Name of property</param>
        /// <param name="value">Value for comparison</param>
        /// <returns>Lambda expression</returns>
        public static Expression<Func<T, bool>> CreateLambdaEqual<T>(string propertyName, object value)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            Expression property = Expression.Property(parameter, propertyName);
            Expression target = Expression.Constant(value);
            Expression equalsMethod = Expression.Call(property, "Equals", null, target);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(equalsMethod, parameter);
            return lambda;
        }

        /// <summary>
        /// Set value to property of target object
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="target">Target object which property will be set</param>
        /// <param name="propertyName">Property name that should be set</param>
        /// <param name="value">Value that will be set to property</param>
        public static void SetPropertyValue<TTarget>(this TTarget target, string propertyName, object value)
        {
            if (target == null)
            {
                throw new ArgumentException("Calling SetPropertyValue on null instance.");
            }
            if (typeof(TTarget).GetProperty(propertyName) == null)
            {
               throw new ArgumentException("Invalid property name.");
            }
            typeof(TTarget).GetProperty(propertyName).SetValue(target, value, null);
        }


        /// <summary>
        /// Get value of property of target object
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="target">Target object which property will be get</param>
        /// <param name="propertyName">Property name that should be get</param>
        public static TReturn GetPropertyValue<TTarget, TReturn>(this TTarget target, string propertyName)
        {
            var propType = typeof(TTarget).GetProperty(propertyName);
            if (propType == null)
            {
                throw new ArgumentException("Property not found from object.");
            }
            if (!typeof(TReturn).IsAssignableFrom(propType.PropertyType))
            {
                return default(TReturn);
            }
            return (TReturn) propType.GetValue(target);
        }

        /// <summary>
        /// Get value of property of target object
        /// </summary>
        /// <param name="target">Target object which property will be get</param>
        /// <param name="propertyName">Property name that should be get</param>
        /// <returns>Object value of desired target and its property</returns>
        public static object GetPropertyObjectValue(this object target, string propertyName)
        {
            var property = target.GetType().GetProperty(propertyName);
            if (property == null)
            {
                throw new ArgumentException("Known invalid property name.");
            }
            return property.GetValue(target);
        }


        /// <summary>
        /// Checks if enumeration is empty. ie containing no value or it is null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        public static bool IsNullOrWhitespace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        /// Convert readonly list  to list. Converstion done by type checking, if fails, then it is converted by ToList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="readOnlyList">Readonly list that will be converted</param>
        /// <returns>list of List type</returns>
        public static List<T> InclusiveToList<T>(this IReadOnlyList<T> readOnlyList)
        {
            return readOnlyList as List<T> ?? readOnlyList?.ToList();
        }


        /// <summary>
        /// Add item to collection and return it immediately
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">Target collection where item will be added</param>
        /// <param name="item">Item to Add</param>
        /// <returns>Added item</returns>
        public static T AddAndReturn<T>(this ICollection<T> collection, T item)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection", "Value cannot be null.");
            }
            collection.Add(item);
            return item;
        }

        /// <summary>
        /// Checks if target type is Enumerable type, i.e. any type that implements IEnumerable interface
        /// </summary>
        /// <param name="target">Target that will be check if it is IEnumerable</param>
        /// <param name="exceptions">List of Types that will be considered as exceptions, i.e. if type is in exception list, it will return False</param>
        /// <returns>True if type implements IEnumerable</returns>
        public static bool IsEnumerable(this Type target, List<Type> exceptions = null)
        {
            if (exceptions?.Contains(target) == true)
            {
                return false;
            }
            var checkedTypes = target.GetInterfaces().ToList();
            checkedTypes.Add(target);
            return checkedTypes.Any(intType => intType.GetTypeInfo().IsGenericType && intType.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }


        public static bool IsCollection(this Type target)
        {
            var checkedTypes = target.GetInterfaces().ToList();
            checkedTypes.Add(target);
            return checkedTypes.Any(intType => intType.GetTypeInfo().IsGenericType && intType.GetGenericTypeDefinition() == typeof(ICollection<>));
        }

        /// <summary>
        /// Run action in separated thread and wait for its exit
        /// </summary>
        /// <param name="action">Action that will be called in thread</param>
        public static void RunInThreadAndWait(Action action)
        {
            if (action == null) return;
            Exception dbException = null;
            Thread workerThread = new Thread(() => action()) { IsBackground = false };
            workerThread.Start();
            workerThread.Join();
            if (dbException != null)
            {
                throw dbException;
            }
        }

        /// <summary>
        /// Guid extension checking if Guid is assigned, i.e. it is not null and not empty Guid
        /// </summary>
        /// <param name="guid">Guid that will be tested</param>
        /// <returns>True if valid Guid is assigned</returns>
        public static bool IsAssigned(this Guid? guid)
        {
            return guid.HasValue && guid.Value.IsAssigned();
        }

        /// <summary>
        /// Guid extension checking if Guid is assigned, i.e. it is not null and not empty Guid
        /// </summary>
        /// <param name="guid">Guid that will be tested</param>
        /// <returns>True if valid Guid is assigned</returns>
        public static bool IsAssigned(this Guid guid)
        {
            return guid != Guid.Empty;
        }

        /// <summary>
        /// Calculate Guid from string
        /// </summary>
        /// <param name="str">Input string that will be used as input for Guid calculation</param>
        /// <param name="forType">Type of entity for which the GUID will be generated, influences hash calcuation</param>
        /// <returns>Guid from string</returns>
        public static Guid GetGuid(this string str, string forType = null)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes((String.IsNullOrEmpty(forType) ? String.Empty : forType) + str));
                return new Guid(hash);
            }
        }

        /// <summary>
        /// Calculate Guid from string
        /// </summary>
        /// <typeparam name="ForType">Type of entity for which the GUID will be generated, influences hash calcuation</typeparam>
        /// <param name="str">Input string that will be used as input for Guid calculation</param>
        /// <returns>Guid from string</returns>
        public static Guid GetGuid<ForType>(this string str)
        {
            return GetGuid(str, typeof(ForType).Name);
        }

        /// <summary>
        /// Calculate Guid from string
        /// </summary>
        /// <param name="str">Input string that will be used as input for Guid calculation</param>
        /// <param name="forType">Type of entity for which the GUID will be generated, influences hash calcuation</param>
        /// <returns>Guid from string</returns>
        public static Guid GetGuid(this string str, Type forType)
        {
            return GetGuid(str, forType?.Name);
        }

        /// <summary>
        /// String extension parsing content into Guid
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Guid if string is Guid or null if it is invalid</returns>
        public static Guid? ParseToGuid(this string str)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Guid id;
            if (Guid.TryParse(str, out id) && id != Guid.Empty)
            {
                return id;
            }
            return null as Guid?;
        }

        /// <summary>
        /// Dictionary extension for trying get value by key, returns default value if does not exist
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary">Input dictionary</param>
        /// <param name="key">Key for selecting item from dictionary</param>
        /// <returns>Value from dictionary or type's default value if does not exist</returns>
        public static TValue TryGet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("Dictionary can not be null.");
            }
            return TryGetImpl(dictionary, key);
        }

        /// <summary>
        /// Dictionary extension for trying get value by key, returns default value if does not exist
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary">Input dictionary</param>
        /// <param name="key">Key for selecting item from dictionary</param>
        /// <returns>Value from dictionary or type's default value if does not exist</returns>
        public static TValue TryGet<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("Dictionary can not be null.");
            }
            return TryGetImpl(dictionary, key);
        }

        /// <summary>
        /// Dictionary extension for trying get value by key, returns default value if does not exist
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary">Input dictionary</param>
        /// <param name="key">Key for selecting item from dictionary</param>
        /// <param name="defaultValue">Default value if not found</param>
        /// <returns>Value from dictionary or type's default value if does not exist</returns>
        public static TValue TryGetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            return TryGetImplWithDefault(dictionary, key, defaultValue);
        }

        /// <summary>
        /// Dictionary extension for trying get value by key, returns default value if does not exist
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary">Input dictionary</param>
        /// <param name="key">Key for selecting item from dictionary</param>
        /// <returns>Value from dictionary or type's default value if does not exist</returns>
        private static TValue TryGetImpl<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return TryGetImplWithDefault(dictionary, key, default(TValue));
        }

        /// <summary>
        /// Dictionary extension for trying get value by key, returns default value if does not exist
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary">Input dictionary</param>
        /// <param name="key">Key for selecting item from dictionary</param>
        /// <param name="defaultValue">Default value if not found</param>
        /// <returns>Value from dictionary or type's default value if does not exist</returns>
        private static TValue TryGetImplWithDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            TValue value;
            if (dictionary.TryGetValue(key, out value))
            {
                return value;
            }
            return defaultValue;
        }

        /// <summary>
        /// Read whole hierarchy of exceptions and put them and their message to target string
        /// </summary>
        /// <param name="excp">Exception that will be extracted</param>
        /// <returns>String containing all inner exceptions and their messages</returns>
        public static string ExtractAllInnerExceptions(Exception excp)
        {
            if (excp == null) return String.Empty;
            return excp.GetType().Name + " : " + excp.Message + "\n"+ ExtractAllInnerExceptions(excp.InnerException);
        }

        /// <summary>
        /// Call specific action and repeat it specified times if exception occured or false returned
        /// </summary>
        /// <param name="retries">How many time the action should be repeated</param>
        /// <param name="action">Action that will be repeated if needed</param>
        public static void RunWithRetries(int retries, Func<bool> action)
        {
            if (retries <= 0)
            {
                throw new ArgumentException("Invalid value given for retries count.");
            }

            int tries = 0;
            while (tries++ < retries)
            {
                try
                {
                    if (action())
                    {
                        return;
                    }
                }
                catch (Exception)
                {}
            }
        }

        public static IEnumerable<T> Flatten<T, R>(this IEnumerable<T> source, Func<T, R> recursion) where R : IEnumerable<T>
        {
            var flattened = source.ToList();

            var children = source.Select(recursion);

            if (children == null) return flattened;

            foreach (var child in children)
            {
                flattened.AddRange(child.Flatten(recursion));
            }

            return flattened;
        }

        public static Stream Compress(this Stream decompressedStream, CompressionLevel compressionLevel = CompressionLevel.Optimal)
        {
            var compressed = new MemoryStream();
            using (var zip = new GZipStream(compressed, compressionLevel, true))
            {
                decompressedStream.CopyTo(zip);
            }

            compressed.Seek(0, SeekOrigin.Begin);
            return compressed;
        }

        public static Stream Decompress(this Stream compressedStream)
        {
            var decompressed = new MemoryStream();
            using (var zip = new GZipStream(compressedStream, CompressionMode.Decompress, true))
            {
                zip.CopyTo(decompressed);
            }

            decompressed.Seek(0, SeekOrigin.Begin);
            return decompressed;
        }

        public static string SerializeObject<T>(this T toSerialize)
        {
            if (toSerialize == null)
            {
                throw new ArgumentNullException("Object to serialize is null.");
            }
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }

        public static T DeserializeObject<T>(this string toDeserialize)
        {
            if (toDeserialize == null)
            {
                throw new ArgumentNullException("String to deserialize is null.");
            }
            if (string.IsNullOrWhiteSpace(toDeserialize))
            {
                throw new ArgumentException("String to deserialize is empty or whitespaces.","toDeserialize");
            }
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (StringReader textReader = new StringReader(toDeserialize))
            {
                return (T)xmlSerializer.Deserialize(textReader);
            }
        }

        public static string ToXmlString(this RSACryptoServiceProvider rsa)
        {
            RSAParameters parameters = rsa.ExportParameters(true);

            return String.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
                Convert.ToBase64String(parameters.Modulus),
                Convert.ToBase64String(parameters.Exponent),
                Convert.ToBase64String(parameters.P),
                Convert.ToBase64String(parameters.Q),
                Convert.ToBase64String(parameters.DP),
                Convert.ToBase64String(parameters.DQ),
                Convert.ToBase64String(parameters.InverseQ),
                Convert.ToBase64String(parameters.D));
        }

        public static void FromXmlString(this RSA rsa, string xmlString)
        {
            RSAParameters parameters = new RSAParameters();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Modulus": parameters.Modulus = Convert.FromBase64String(node.InnerText); break;
                        case "Exponent": parameters.Exponent = Convert.FromBase64String(node.InnerText); break;
                        case "P": parameters.P = Convert.FromBase64String(node.InnerText); break;
                        case "Q": parameters.Q = Convert.FromBase64String(node.InnerText); break;
                        case "DP": parameters.DP = Convert.FromBase64String(node.InnerText); break;
                        case "DQ": parameters.DQ = Convert.FromBase64String(node.InnerText); break;
                        case "InverseQ": parameters.InverseQ = Convert.FromBase64String(node.InnerText); break;
                        case "D": parameters.D = Convert.FromBase64String(node.InnerText); break;
                    }
                }
            }
            else
            {
                throw new Exception("Invalid XML RSA key.");
            }

            rsa.ImportParameters(parameters);
        }

        public static int? ParseToInt(this string str)
        {
            int result;
            if (!Int32.TryParse(str, out result)) return null;
            return result;
        }

        public static double? ParseToDouble(this string str)
        {
            double result;
            if (!Double.TryParse(str, out result)) return null;
            return result;
        }

        public static string GetFilePath(this IHostingEnvironment env, string devRootDirectory, string fileName)
        {
            return env.IsDevelopment() ? Path.Combine(devRootDirectory, fileName) : Path.Combine(env.ContentRootPath, fileName);
        }

        /// <summary>
        /// Parses to unique identifier with exeption.
        /// </summary>
        /// <param name="stringGuid">The string unique identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public static Guid ParseToGuidWithExeption(this string stringGuid)
        {
            Guid? guid = stringGuid.ParseToGuid();
            if (guid == null)
            {
                throw new Exception($"Cannot parse '{stringGuid}' to type of Guid.");
            }
            return guid.Value;
        }

        /// <summary>
        /// Parses the specified enum value.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="enumValue">The enum value.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.Exception"></exception>
        public static TEnum Parse<TEnum>(this string enumValue)
        {
            if (!typeof(TEnum).GetTypeInfo().IsEnum) throw new PtvArgumentException(CoreMessages.OpenApi.NotEnum);

            try
            {
                return (TEnum)Enum.Parse(typeof(TEnum), enumValue);
            }
            catch(Exception)
            {
                throw new Exception(String.Format(CoreMessages.OpenApi.RequestMalFormatted, typeof(TEnum).Name, GetEnumvaluesAsList<TEnum>()));
            }
        }

        private static string GetEnumvaluesAsList<TEnum>()
        {
            return String.Join(", ", Enum.GetNames(typeof(TEnum)));
        }


        public static string ConvertToString(this TimeSpan timeSpan)
        {
            return string.Format("{0}:{1:D2}:{2:D2}", (int) timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
        }


        public static int PositiveOrZero(this int integer)
        {
            return integer > 0 ? integer : 0;
        }

        /// <summary>
        /// Helper to check if the are more results available (used when results are paged).
        /// </summary>
        /// <param name="totalResults">How many results there are</param>
        /// <param name="pageNumber">page number of the results (first page is assumed to be number 0)</param>
        /// <param name="pageSize">how many items are on per page</param>
        /// <returns>true if there are more "page" results left (totalResults > pagesize * pagenumber) otherwise false</returns>
        /// <remarks>
        /// <para>If <paramref name="totalResults"/> is less than one returns false.</para>
        /// <para>If <paramref name="pageNumber"/> is less than zero returns false</para>
        /// <para>If <paramref name="pageSize"/> is less than one returns false</para>
        /// </remarks>
        public static bool MoreResultsAvailable(this int totalResults, int pageNumber, int pageSize = CoreConstants.MaximumNumberOfAllItems)
        {
            bool moreAvailable = false;

            if (totalResults > 0 && pageNumber >= 0 && pageSize > 0)
            {
                if (pageNumber == 0)
                {
                    // first page
                    moreAvailable = totalResults > pageSize;
                }
                else
                {
                    // page number used as zero indexed so just add one to it to be able to calculate correctly results left (++pagenumber add one before multiplying)
                    moreAvailable = totalResults > ++pageNumber * pageSize;
                }
            }

            return moreAvailable;
        }

        // Convert the string to camel case.
        public static string ToCamelCase(this Enum enumVar)
        {
            var stringEnum = enumVar.ToString();
            // If there are 0 or 1 characters, just return the string.
            if (stringEnum == null || stringEnum.Length < 2)
                return stringEnum;

            // Split the string into words.
            var words = stringEnum.Split('_');
            
            // Combine the words.
            var result = char.ToLowerInvariant(words[0][0]) + words[0].Substring(1);
            for (var i = 1; i < words.Length; i++)
            {
                result +=
                    words[i].Substring(0, 1).ToUpper() +
                    words[i].Substring(1);
            }

            return result;
        }

        public static void RemoveWhere<T>(this IList<T> list, Func<T, bool> condition)
        {
            var toRemove = list.Where(condition).ToList();
            toRemove.ForEach(i => list.Remove(i));
        }

        public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> dictA, IDictionary<TKey, TValue> dictB)
        {
            return dictA.Keys.Union(dictB.Keys).Distinct().ToDictionary(k => k, k => dictA.ContainsKey(k) ? dictA[k] : dictB[k]);
        }

        public static Dictionary<TKey, Tuple<TValue, TValue>> Cross<TKey, TValue>(this IDictionary<TKey, TValue> dicA, IDictionary<TKey, TValue> dicB)
        {
            return dicA.Keys.Where(dicB.ContainsKey).ToDictionary(k => k, k => new Tuple<TValue , TValue>( dicA[k], dicB[k] ));
        }

        public static string ConvertToString(this IList<Guid> guidList)
        {
            return String.Join(", ", guidList);
        }

        public static List<string> SplitCsv(this string csvList, bool nullOrWhitespaceInputReturnsNull = false)
        {
            if (string.IsNullOrWhiteSpace(csvList))
                return nullOrWhitespaceInputReturnsNull ? null : new List<string>();

            return csvList
                .TrimEnd(',')
                .Split(',')
                .Select(s => s.Trim())
                .ToList();
        }


        public static IEnumerable<T> DistinctBy<T>(this IEnumerable<T> enumerable, Func<T, object> compareBy)
        {
            return enumerable.Distinct(new DistinctEqualityComparer<T>(compareBy));
        }

        internal class DistinctEqualityComparer<T> : IEqualityComparer<T>
        {
            private readonly Func<T, object> compareByLambda;

            public DistinctEqualityComparer(Func<T, object> compareBy)
            {
                this.compareByLambda = compareBy;
            }

            public bool Equals(T x, T y)
            {
                return compareByLambda(x) == compareByLambda(y);
            }

            public int GetHashCode(T obj)
            {
                return compareByLambda(obj).GetHashCode();
            }
        }

    }
}
