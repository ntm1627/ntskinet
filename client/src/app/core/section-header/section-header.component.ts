import { Observable } from 'rxjs';
import { BreadcrumbService } from 'xng-breadcrumb';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-section-header',
  templateUrl: './section-header.component.html',
  styleUrls: ['./section-header.component.scss']
})
export class SectionHeaderComponent implements OnInit {
  breadcrumb$: Observable<any[]>;

  constructor(private bcService: BreadcrumbService) { }

  //in the shop service the unsubscribe/clean up action is made by angular itself as it is an http request
  // in this case since this is not an http request we are required to dispose the resource. So that is why the user
  // defined observable used as work around
  ngOnInit(): void {
    this.breadcrumb$=this.bcService.breadcrumbs$ //we are populating our property observable with one that came from the service
  }

}
