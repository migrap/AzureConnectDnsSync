using FluentScheduler.Model;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AzureConnectDnsSync {
    internal static partial class Extensions {
        private static readonly Lazy<ConcurrentDictionary<Type, Logger>> _loggers = new Lazy<ConcurrentDictionary<Type, Logger>>(() => new ConcurrentDictionary<Type, Logger>());

        public static Logger Log<T>(this T self) {
            return _loggers.Value.GetOrAdd(typeof(T), (t) => LogManager.GetLogger(t.FullName));
        }

        public static void Debug(this Logger self, Func<string> func) {
            if (self.IsDebugEnabled) {
                self.Debug(func());
            }
        }

        public static void Error(this Logger self, Func<string> func) {
            if (self.IsErrorEnabled) {
                self.Error(func());
            }
        }

        public static void Fatal(this Logger self, Func<string> func) {
            if (self.IsFatalEnabled) {
                self.Fatal(func());
            }
        }

        public static void Info(this Logger self, Func<string> func) {
            if (self.IsInfoEnabled) {
                self.Info(func());
            }
        }

        public static void Trace(this Logger self, Func<string> func) {
            if (self.IsTraceEnabled) {
                self.Trace(func());
            }
        }

        public static void Warn(this Logger self, Func<string> func) {
            if (self.IsWarnEnabled) {
                self.Warn(func());
            }
        }

        public static void Foreach<TSource>(this IEnumerable<TSource> source, Action<TSource> action) {
            foreach (var item in source) {
                action(item);
            }
        }

        public static void AndEvery(this SpecificRunTime self, TimeSpan interval) {
            self.AndEvery((int)interval.TotalSeconds).Seconds();
        }
    }
}
