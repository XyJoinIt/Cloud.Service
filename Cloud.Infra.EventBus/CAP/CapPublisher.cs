using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Infra.EventBus.CAP
{
    public class CapPublisher : IEventPublisher
    {
        private readonly ICapPublisher _eventBus;

        public CapPublisher(ICapPublisher capPublisher) => _eventBus = capPublisher;

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public void Publish<T>(string name, T obj, IDictionary<string, string>? headers = null) where T : class
        {
            _eventBus.Publish(name, obj, headers);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task PublishAsync<T>(string name, T obj, IDictionary<string, string>? headers = null) where T : class
        {
            await _eventBus.PublishAsync(name, obj, headers);
        }

        /// <summary>
        /// 延迟消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="timeSpan"></param>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        /// <param name="headers"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void PublishDelayMsg<T>(TimeSpan timeSpan, string name, T obj, IDictionary<string, string>? headers = null) where T : class
        {
            _eventBus.PublishDelay(timeSpan, name, obj, headers);
        }
    }
}
