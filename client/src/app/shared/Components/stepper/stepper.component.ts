import { CdkStepper } from '@angular/cdk/stepper';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-stepper', //Angular Material's stepper provides a wizard-like workflow by dividing content into logical steps
  templateUrl: './stepper.component.html',
  styleUrls: ['./stepper.component.scss'],
  providers: [{provide: CdkStepper, useExisting: StepperComponent}]
})
export class StepperComponent extends CdkStepper implements OnInit {
  @Input() linearModeSelected: boolean;

  ngOnInit(): void {
    this.linear=this.linearModeSelected;  //assign what we selected to the CdkStepper as a true or false
  }

  onClick(index: number){
    this.selectedIndex=index;
  }

}

/*
A selector is used to identify each component uniquely into the component tree,
and it also defines how the current component is represented in the HTML DOM
e.g for stepper component it will be <app-stepper></app-stepper> in checkout.component.html page*/