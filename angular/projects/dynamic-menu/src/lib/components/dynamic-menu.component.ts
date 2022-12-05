import { Component, OnInit } from '@angular/core';
import { DynamicMenuService } from '../services/dynamic-menu.service';

@Component({
  selector: 'lib-dynamic-menu',
  template: ` <p>dynamic-menu works!</p> `,
  styles: [],
})
export class DynamicMenuComponent implements OnInit {
  constructor(private service: DynamicMenuService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
