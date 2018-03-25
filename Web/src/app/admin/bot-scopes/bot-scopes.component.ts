import { Component, OnInit } from '@angular/core';
import { BotsResponse, AdminService } from '../../../swagger-gen';

@Component({
  selector: 'app-bot-scopes',
  templateUrl: './bot-scopes.component.html',
  styleUrls: ['./bot-scopes.component.scss']
})
export class BotScopesComponent implements OnInit {
  public botsResponse: BotsResponse[] = [];
  constructor(private adminService: AdminService) { }

  ngOnInit() {
    this.adminService.adminBotsGet()
      .subscribe(response => this.botsResponse = response);
  }
}
