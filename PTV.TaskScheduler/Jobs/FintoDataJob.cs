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
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PTV.Database.DataAccess.Interfaces.Services;
using PTV.Database.Model.Models.Base;
using PTV.DataImport.ConsoleApp.Tasks;
using PTV.TaskScheduler.Configuration;
using Quartz;

namespace PTV.TaskScheduler.Jobs
{
    
    internal abstract class FintoDataJob<TEntity> : BaseJob 
        where TEntity : FintoItemBase<TEntity>, new()
    {
        public Task Execute(IJobExecutionContext context)
        {
            var serviceProvider = context.Scheduler.Context.Get(QuartzScheduler.SERVICE_PROVIDER) as IServiceProvider;
            var jobData = QuartzScheduler.GetJobDataConfiguration<FintoDataJobDataConfiguration>(context.JobDetail);
            ExecuteInternal(() =>
            {
                var settings = FintoJobExtension.GetSettings(jobData.DownloadType);
                var typeSettings = FintoJobExtension.SetDownloadDefinition(settings, jobData.DownloadType);

                var downloadFintoTask = new DownloadFintoDataTask();
                downloadFintoTask.DownloadData(settings).Wait();
                if (typeSettings.MergeTo == null) return;
                
                var service = serviceProvider.GetService<ISeedingService>();
                service?.SeedFintoItems<TEntity>(typeSettings.MergeTo, context.JobDetail.Key.Name);
            }, context);
            return Task.CompletedTask;
        }
    }
}
