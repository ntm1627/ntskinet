import { AccountService } from 'src/app/account/account.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { BasketService } from '../basket/basket.service';
import { Observable } from 'rxjs';
import { IBasketTotals } from '../shared/models/basket';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss'],
})
export class CheckoutComponent implements OnInit {

  checkoutForm: FormGroup; //The FormGroup is a collection of Form controls It Tracks the value and validity state of a group of Form control instances
  basketTotals$: Observable<IBasketTotals>;

  constructor(private fb: FormBuilder, private accountService: AccountService , private basketService: BasketService) {}

  ngOnInit(): void {
    this.createCheckoutForm();  //forms needs to be called in the constructor before use
    this.getAddressFormValues();
    this.getDeliveryMethodValue();
    this.basketTotals$ = this.basketService.basketTotal$;
  }

  //this method validate creates 3 separate forms
  //we can validate each form individually and as a whole
  createCheckoutForm() {
    this.checkoutForm = this.fb.group({
      addressForm: this.fb.group({
        firstName: [null, Validators.required],
        lastName: [null, Validators.required],
        street: [null, Validators.required],
        city: [null, Validators.required],
        state: [null, Validators.required],
        zipcode: [null, Validators.required],
      }),
      deliveryForm: this.fb.group({
        deliveryMethod: [null, Validators.required],
      }),
      paymentForm: this.fb.group({
        nameOnCard: [null, Validators.required],
      }),
    });
  }

  //If the user has address in the server we populate the form
  getAddressFormValues(){
    this.accountService.getUserAddress().subscribe(address =>{
      if(address){
        this.checkoutForm.get('addressForm').patchValue(address);
      }
    }, error => {
        console.log(error);
    });
  }

  getDeliveryMethodValue(){
    const basket= this.basketService.getCurrentBasketValue();
    if(basket.deliveryMethodId !== null){
      this.checkoutForm.get('deliveryForm').get('deliveryMethod').patchValue(basket.deliveryMethodId.toString());
    }

  }
}
