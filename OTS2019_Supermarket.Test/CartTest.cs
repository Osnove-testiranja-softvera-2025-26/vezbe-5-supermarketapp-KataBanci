using NUnit.Framework;
using OTS_Supermarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS_Supermarket.Test
{
    [TestFixture]
    public class CartTest
    {
        [Test]
        public void AddOneToCard_ShouldAddItemToCart_Success() //naziv klasse koju testiram sta testiram i sta ocekujem
        {

            //ARRANGE pred uslovi

            Cart cart = new Cart();
            //ako dodam da je cart.size=5 onda dole ne moze biti 1 nego ce biti 6 
            Monitor monitor = new Monitor();

            //ACT poziv metode koje zelimo da testiramo

            cart.AddOneToCart(monitor);

            //ASSERT nesto gde treba da proverimo, da testiramo

            Assert.That(cart.Size, Is.EqualTo(1)); //druga vredsnost je sta ocekujem da bude
            Assert.That(cart.Amount, Is.EqualTo(100));


        }
    }
}
