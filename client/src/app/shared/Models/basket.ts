 import {v4 as uuidv4} from 'uuid'

  export interface IBasketItem {
      id: number;
      productName: string;
      price: number;
      quantity: number;
      pictureUrl: string;
      brand: string;
      type: string;
  }

  export interface IBasket {
      id: string;
      items: IBasketItem[];
      clientSecret?: string;
      paymentIntentId?: string;
      deliveryMethodId?: number;
      shippingPrice?: number;
  }

  export class Basket implements IBasket{
    id= uuidv4();  //Version 4 UUID is a universally unique identifier that is generated using random numbers
    items: IBasketItem[]=[];

  }

  export interface IBasketTotals{
    shipping: number;
    subtotal: number;
    total: number;
  }
