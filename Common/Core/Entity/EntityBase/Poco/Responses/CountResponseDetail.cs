﻿namespace EntityBase.Poco.Responses
{
    /// <summary>
    /// Count sorgularını bu model ile dönüyoruz
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CountResponseDetail<T>
    {
        /// <summary>
        /// Hangi verinin sayısını kontrol ediyoruz
        /// </summary>
        public T Item { get; set; }

        /// <summary>
        /// Var mı bilgisi
        /// </summary>
        public long Count { get; set; }
    }
}
