using System;
using System.Collections.Generic;

namespace SingleUseWorld
{
    public class WeightedProbability<T>
    {
        private struct WeightedItem
        {
            public T Obj;
            public double Weight;

            public WeightedItem(T obj, double weight)
            {
                Obj = obj;
                Weight = weight;
            }
        }

        private List<WeightedItem> _weightedItems;
        private double _totalWeight;
        private Random _random;

        public WeightedProbability()
        {
            _weightedItems = new List<WeightedItem>();
            _totalWeight = 0;
            _random = new Random();
        }

        public void Add(T obj, double weight)
        {
            _totalWeight += weight;
            var item = new WeightedItem(obj, _totalWeight);
            _weightedItems.Add(item);
        }

        public void Clear()
        {
            _totalWeight = 0;
            _weightedItems.Clear();
        }

        public T Next()
        {
            double randomWeight = _random.NextDouble() * _totalWeight;

            foreach (var item in _weightedItems)
                if (item.Weight >= randomWeight)
                    return item.Obj;

            return default(T);
        }
    }
}
