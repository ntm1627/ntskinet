import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-paging-header',
  templateUrl: './paging-header.component.html',
  styleUrls: ['./paging-header.component.scss']
})
export class PagingHeaderComponent implements OnInit {
  // @Input() allows a parent component to update data in the child component. Conversely, @Output() allows the child to send data to a parent component.
  @Input() pageNumber:number;
  @Input() pageSize: number;
  @Input() totalCount:number;


  constructor() { }

  ngOnInit(): void {
  }

}
