using System;
using System.Collections.Generic;
using UnityEngine;

namespace SingleUseWorld
{
    public class MonoPool<TPrefab> where TPrefab : MonoBehaviour, IPoolable
    {
        public enum ExpandMethod
        {
            Disabled,
            OneAtATime,
            Doubling
        }

        #region Fields
        private MonoFactory<TPrefab> _factory;
        private Transform _container;

        private Stack<TPrefab> _availableItems;
        private int _occupiedItemsCount;
        
        private int _initialSize;
        private int _maxSize;
        private ExpandMethod _expandMethod;
        #endregion

        #region Properties
        public int AvailableCount
        {
            get { return _availableItems.Count; }
        }

        public int OccupiedCount
        {
            get { return _occupiedItemsCount; }
        }

        public int TotalCount
        {
            get { return AvailableCount + OccupiedCount; }
        }
        #endregion

        #region Construcors
        public MonoPool(MonoFactory<TPrefab> factory, int initialSize)
        {
            _factory = factory;
            _initialSize = initialSize;

            _availableItems = new Stack<TPrefab>(_initialSize);
            Resize(initialSize);
        }
        #endregion

        #region Public Methods
        public TPrefab Get()
        {
            if(AvailableCount == 0)
                ExpandPool();

            var item = _availableItems.Pop();
            _occupiedItemsCount++;

            item.gameObject.SetActive(true);
            item.Reinitialize();

            return item;
        }

        public void Release(TPrefab item)
        {
            _availableItems.Push(item);
            _occupiedItemsCount--;

            item.gameObject.SetActive(false);
            SetContainer(item);

            if (AvailableCount > _maxSize)
                Resize(_maxSize);
        }

        public void Resize(int desiredPoolSize)
        {
            if (AvailableCount == desiredPoolSize)
                return;

            if (_expandMethod == ExpandMethod.Disabled)
                throw new Exception(String.Format("Pool '{0}' attempted resize but pool set to fixed size of '{1}'!", GetType(), AvailableCount));

            if (desiredPoolSize < 0)
                throw new Exception(String.Format("Pool '{0}' attempted to resize to a negative amount!", GetType()));

            while (AvailableCount > desiredPoolSize)
            {
                DeallocateItem();
            }

            while (AvailableCount < desiredPoolSize)
            {
                AllocateItem();
            }
        }

        private void Clear()
        {
            Resize(0);
        }

        private void ExpandBy(int numberToAdd)
        {
            Resize(AvailableCount + numberToAdd);
        }

        private void ShrinkBy(int numberToRemove)
        {
            Resize(AvailableCount - numberToRemove);
        }
        #endregion

        #region Private Methods
        private void ExpandPool()
        {
            switch (_expandMethod)
            {
                case ExpandMethod.Disabled:
                    {
                        throw new Exception(String.Format("Pool '{0}' attempted expand but pool set to fixed size of '{1}'!", GetType(), AvailableCount));
                    }
                case ExpandMethod.OneAtATime:
                    {
                        ExpandBy(1);
                        break;
                    }
                case ExpandMethod.Doubling:
                    {
                        if(TotalCount == 0)
                        {
                            ExpandBy(1);
                        }
                        else
                        {
                            ExpandBy(TotalCount);
                        }
                        break;
                    }
            }
        }

        private void AllocateItem()
        {
            var item = _factory.Create();
            _availableItems.Push(item);

            item.gameObject.SetActive(false);
            SetContainer(item);
        }

        private void DeallocateItem()
        {
            var item = _availableItems.Pop();
            _factory.Destroy(item);
        }

        private void SetContainer(TPrefab item)
        {
            if (item.transform.parent != _container)
                item.transform.SetParent(_container);
        }
        #endregion
    }
}
