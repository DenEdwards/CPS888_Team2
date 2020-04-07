import * as React from "react";
import * as ReactDOM from "react-dom";
import { FoodModel, IAppState } from "./Models";
import { Popup } from "./Popup";

export class OrderList extends React.Component<any, IAppState> {
    state: { items: any; myOrder: any; showPopup: boolean; userId: number; orderPlaced: boolean; };
    setState: any;
    constructor(state) {
        super(state);
        this.state = { items: null, myOrder: null, showPopup: false, userId: 0, orderPlaced: false };
        this.getLoginStatus();
        this.loadMenusFromServer();
        this.handleDataFromChild == this.handleDataFromChild.bind(this);
    }

    handleDataFromChild(popupShown, isOrderPlaced) {
        var tmp: IAppState = this.state;

        if (isOrderPlaced) {
            tmp.myOrder = null;
            tmp.orderPlaced = true;
            tmp.showPopup = false;
        }
        else {
            tmp.orderPlaced = false;
            tmp.showPopup = false;
        }
        this.setState(tmp);
        document.getElementById('dvcart').style.visibility = 'visible';

    }

    getLoginStatus() {
        var xhr = new XMLHttpRequest();
        xhr.open('get', '/data/GetUserId/', true);
        xhr.onload = function () {
            var userid: number = parseInt(xhr.responseText);
            var tmp: IAppState = this.state;
            tmp.userId = userid;
            this.setState(tmp);

        }.bind(this);
        xhr.send();
    }
    //here's where we get the data from the server, you see the API endpoint /data/GetMenuList/
    loadMenusFromServer() {
        var xhr = new XMLHttpRequest();
        xhr.open('get', '/data/GetMenuList/', true);
        xhr.onload = function () {
            var dataitems = JSON.parse(xhr.responseText);
            var tmp: IAppState = this.state;
            tmp.items = dataitems;
            this.setState(tmp);
        }.bind(this);
        xhr.send();
    }

    addToCart(id) {
        if (this.state.userId < 1) {
            alert('Log in to continue!');
            return;
        }
        id--;
        var myCart = this.state.myOrder || [];
        var allItems = this.state.items;
        if (myCart.indexOf(allItems[id]) > -1) {
            var itemToOrder = myCart.find(m => m.Id === allItems[id].Id);
            itemToOrder["Quantity"] = itemToOrder["Quantity"] + 1;
        }
        else {
            var itemToOrder = allItems[id];
            itemToOrder["Quantity"] = 1;
            myCart.push(allItems[id]);
        }

        var tmp: IAppState = this.state;
        tmp.myOrder = myCart;
        tmp.showPopup = false;
        this.setState(tmp);
    } 

    removeFromCart(id) {
        if (this.state.userId < 1) {
            alert('Log in to continue!');
            return;
        }
        var myCart = this.state.myOrder || [];
        var allItems = this.state.items;
        myCart.splice(id, 1);

        var tmp: IAppState = this.state;
        tmp.myOrder = myCart;
        this.setState(tmp);
    }

    continueOrder() {
        var tmp: IAppState = this.state;
        tmp.showPopup = true;
        this.setState(tmp);
        document.getElementById('dvcart').style.visibility = 'hidden';
    }


    render() {
        let menus = this.state.items || [];
        var menuList = menus.map(function (menu) {
            return (
                <div key={menu.Id}>
                    <b>{menu.Name} </b>    <br />
                    <img style={{ width: '100px', float: 'left', margin: '5px' }} src={"/img/" + menu.Picture} />{menu.Description}<p />
                    <div>
                        ${menu.Price} | <a href='#' onClick={this.addToCart.bind(this, menu.Id)}>Add to Cart</a>
                    </div><hr />
                </div>
                )
        }, this);

        var total = 0;
        var cartItemIndex = 0;
        let myCart = this.state.myOrder || [];

        var myItems = myCart.map(function (menu) {
            total += menu.Price * menu.Quantity;
            return (
                <div key={menu.Id}>
                    <img style={{ width: '75px', float: 'left', margin: '5px' }} src={"/img/" + menu.Picture} />
                    {menu.Name}<br />
                    Qty: {menu.Quantity}<br />
                    Price: ${menu.Price * menu.Quantity} <br />
                    | <a href='#' onClick={this.removeFromCart.bind(this, cartItemIndex++)}>Remove Item</a>
                    <hr />
                </div>

            );

        }, this);

        var totalAndContinueLink = <div className="grandTotal cartEmpty">The Cart is Empty</div>;
        if (total > 0)
            totalAndContinueLink =
                <div className="grandTotal cartNotEmpty">Grand Total: ${total}
                    <button className="greenBtn continueOrder" onClick={this.continueOrder.bind(this)}>Continue Order</button>
                </div>;

        var cart = document.getElementById("dvcart");
        var menu = document.getElementById("dvmenu");

        if (this.state.orderPlaced)
            cart.innerHTML = '<div class="orderPlaced">Order Successfully Placed </div>';

        if (this.state.userId < 1) {
            myItems = null;
            if (cart != null)
                cart.style.display = "none";
            if (menu != null)
                menu.style.flex = "0 0 85%";
        }
        else {
            if (cart != null)
                cart.style.display = "block";
            if (menu != null)
                menu.style.flex = "0 0 55%";
        } 


        return (
            <div>

                {this.state.showPopup ?
                    <Popup
                        handlerFromParent={this.handleDataFromChild}
                        myOrder={this.state.myOrder}
                        userId={this.state.userId} /> : null}

                <div id="wrapper">
                    <div id="dvmenu">
                        {menuList}
                    </div>
                    <div id="dvcart">
                        <div id="cartContent">
                            {myItems}
                        </div>
                        {totalAndContinueLink}
                    </div>
                </div>
            </div>
        );
    }
}