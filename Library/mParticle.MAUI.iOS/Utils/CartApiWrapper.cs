using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;

namespace mParticle.MAUI.iOS.Utils
{
    public class CartWrapper : ICart
    {
        private iOSBinding.MPCart _cart;
        private iOSBinding.MPCommerce _commerce;

        internal CartWrapper(iOSBinding.MPCart cart, iOSBinding.MPCommerce commerce)
        {
            _cart = cart;
            _commerce = commerce;
        }

        public void Add(Product product)
        {
            _cart.AddProduct(Utils.ConvertToMpProduct(product));
        }

        public void AddAll(List<Product> products, bool logEvent)
        {
            NSArray array = new NSArray();
            _cart.AddAllProducts(
                (NSArray<iOSBinding.MPProduct>)NSArray.FromObjects(products
                                                                   .Select(product => Utils.ConvertToMpProduct(product))
                                                                   .ToList()), logEvent);
        }

        public void Checkout()
        {
            _commerce.Checkout();
        }

        public void Checkout(int step, string options)
        {
            _commerce.CheckoutWithOptions(options, step);
        }

        public void Clear()
        {
            _cart.Clear();
        }

        public void GetProduct(string name)
        {
            _cart.Products.FirstOrDefault();
        }

        public List<Product> GetProducts()
        {
            return _cart.Products.Select(product => Utils.ConvertToXamProduct(product)).ToList();
        }

        public void Purchase(TransactionAttributes transactionAttributes, bool clear = false)
        {
            _commerce.PurchaseWithTransactionAttributes(Utils.ConvertToMpTransactionAttributes(transactionAttributes), clear);
        }

        public void Refund(TransactionAttributes transactionAttributes, bool clear)
        {
            _commerce.RefundTransactionAttributes(Utils.ConvertToMpTransactionAttributes(transactionAttributes), clear);
        }

        public void Remove(int index)
        {
            Remove(Utils.ConvertToXamProduct(_cart.Products.ElementAt(index)));
        }

        public void Remove(Product product)
        {
            _cart.RemoveProduct(Utils.ConvertToMpProduct(product));
        }

        public void SetMaximumProductCountAndroid(int max)
        {
            //not implemented
        }
    }
}
