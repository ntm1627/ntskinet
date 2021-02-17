import { IAddress } from './../../shared/Models/address';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/account/account.service';
import { FormGroup } from '@angular/forms';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss']
})
export class CheckoutAddressComponent implements OnInit {
@Input() checkoutForm: FormGroup  //@Input() decorator allows data to be input into the child component from a parent component.

  constructor(private accountService: AccountService, private toaster: ToastrService) { }

  ngOnInit(): void {

  }

  saveUserAddress(){
    this.accountService.updateAddress(this.checkoutForm.get('addressForm').value).subscribe((address: IAddress) =>{
      this.toaster.success('Address Saved');
      this.checkoutForm.get('addressForm').reset(address);  //this makes the button get disabled after we set a value in address
    }, error =>{
      this.toaster.error(error.message);
      console.log(error);
    });
  }



}
