using System;
using System.Collections.Generic;
using System.Linq;

namespace mParticle.Xamarin.Android.Wrappers
{
    public class CartApiWrapper : ICart
    {
        private Android.CommerceBinding.Cart _cart;

        internal CartApiWrapper(Android.CommerceBinding.Cart cart)
        {
            _cart = cart;
        }

        public void Add(Product product)
        {
            _cart.Add(Utils.ConvertToMpProduct(product));
        }

        public void AddAll(List<Product> products, Boolean logEvent)
        {
            _cart.AddAll(products
                         .Select(product => Utils.ConvertToMpProduct(product))
                         .ToList(), logEvent);
        }

        public void Checkout()
        {
            _cart.Checkout();
        }

        public void Checkout(int step, string options)
        {
            _cart.Checkout(step, options);
        }

        public void Clear()
        {
            _cart.Clear();
        }

        public Product GetProduct(string name)
        {
            return Utils.ConvertToXamProduct(_cart.Products().First(product => product.Name.Equals(name)));
        }

        public List<Product> GetProducts()
        {
            return _cart.Products().Select(product => Utils.ConvertToXamProduct(product)).ToList();
        }

        public void Purchase(TransactionAttributes transactionAttributes, bool clear = false)
        {
            _cart.Purchase(Utils.ConvertToMpTransactionAttributes(transactionAttributes), clear);
        }

        public void Refund(TransactionAttributes transactionAttributes, bool clear)
        {
            _cart.Refund(Utils.ConvertToMpTransactionAttributes(transactionAttributes), clear);
        }

        public void Remove(int index)
        {
            _cart.Remove(index);
        }

        public void Remove(Product product)
        {
            _cart.Remove(Utils.ConvertToMpProduct(product));
        }

        public void SetMaximumProductCountAndroid(int max)
        {
            CommerceBinding.Cart.SetMaximumProductCount(max);
        }

        void ICart.GetProduct(string name)
        {
            _cart.Products().FirstOrDefault(product => product.Name.Equals(name));
        }
    }
}
