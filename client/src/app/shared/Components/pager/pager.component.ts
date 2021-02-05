import { shopParams } from './../../Models/shopParams';
import { Component, OnInit,Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent implements OnInit {
  // Input is receiving (it receive the values) from the parent and output is sending to the parent.
  // In our case pager  is a child and Shop component is a parent

@Input() totalCount: number;
@Input() pageSize:number;
@Output() pageChanged=new EventEmitter<number>()  // in output we need to use EventEmitter
  constructor() { }

  ngOnInit(): void {
  }

  onPagerChange(event: any){
    this.pageChanged.emit(event.page);
  }

}
