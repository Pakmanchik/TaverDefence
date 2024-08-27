using System;

namespace DI.UnityZenject
{
    /// <summary>
    /// Система защиты от прямого вызова Bind после вызова Resolve
    /// </summary>
    public sealed class PostBindingGuard
    {
        /// <summary>
        /// Флаг взведенной защиты. Отображает вызвали Resolve хотя бы раз или нет
        /// </summary>
        private bool _isResolveCalled = false;

        /// <summary>
        /// Тип сервиса, полученного при первом вызове Resolve. Для логирования
        /// </summary>
        private Type _firstResolvedService = null;

        /// <summary>
        /// Метод проверки факта пост-вызова Bind(). Должен быть в любой реализации Bind()
        /// </summary>
        public void Check<TService>()
        {
            if (_isResolveCalled)
                throw new InvalidOperationException(
                    $"[{nameof(ZenjectContainer)}] Bind<{typeof(TService).Name}>() called after " +
                    $"Resolve<{_firstResolvedService.Name}>()");
        }

        /// <summary>
        /// Метод взведения защиты. Должен быть в любой реализации Resolve
        /// </summary>
        /// <remarks>
        /// Важен только первый вызов ArmGuard, когда взводится защита и сохраняется тип полученного сервиса
        /// </remarks>
        public void ArmGuard<TService>()
        {
            // Если защита уже взведена, то просто выходим
            if (_isResolveCalled)
                return;

            // Взводим флаг и сохраняем тип сервиса, вызванного в Resolve
            _isResolveCalled = true;
            _firstResolvedService = typeof(TService);
        }
    }
}