export interface FoodModel{

	Id: number;
	Name: string;
	Description: string;
	Picture: string;
	Price: number;
	Quantity: number;
}
//state class
export interface IAppState {
	items: FoodModel[]; //food items that are available for purchase
	myOrder: FoodModel[]; //holds the items the customer wants to purchase
	showPopup: boolean;
	userId: number;
	orderPlaced: boolean; //boolean to indicate if user already placed order
}