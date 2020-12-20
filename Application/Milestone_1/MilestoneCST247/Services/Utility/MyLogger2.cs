﻿using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MilestoneCST247.Services.Utility
{
    public class MyLogger2 : ILogger
    {
        private Logger logger;



        public void Debug(string message, string arg = null)
        {
            if (arg == null)
            {
                GetLogger("myAppLoggerRules").Debug(message);
            }
            else
            {
                GetLogger("myAppLoggerRules").Debug(message, arg);
            }
        }

        public void Error(string message, string arg = null)
        {
            if (arg == null)
            {
                GetLogger("myAppLoggerRules").Debug(message);
            }
            else
            {
                GetLogger("myAppLoggerRules").Debug(message, arg);
            }
        }

        public void Info(string message, string arg = null)
        {
            if (arg == null)
            {
                GetLogger("myAppLoggerRules").Debug(message);
            }
            else
            {
                GetLogger("myAppLoggerRules").Debug(message, arg);
            }
        }

        public void Warning(string message, string arg = null)
        {
            if (arg == null)
            {
                GetLogger("myAppLoggerRules").Debug(message);
            }
            else
            {
                GetLogger("myAppLoggerRules").Debug(message, arg);
            }
        }
        private Logger GetLogger(string thelogger)
        {
            if (logger == null)
            {
                logger = LogManager.GetLogger(thelogger);
            }
            return logger;
        }
    }
}