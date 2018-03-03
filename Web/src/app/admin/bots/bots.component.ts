import { Component, OnInit } from '@angular/core';
import { AdminService, BotsResponse } from '../../../swagger-gen';

@Component({
  selector: 'app-bots',
  templateUrl: './bots.component.html',
  styleUrls: ['./bots.component.scss']
})
export class BotsComponent implements OnInit {
  private botsResponse: BotsResponse[] = [];
  constructor(private adminService: AdminService) { }

  ngOnInit() {
    this.adminService.adminBotsGet()
      .subscribe(response => this.botsResponse = response);
  }
}
