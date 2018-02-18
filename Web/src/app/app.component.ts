import { Component } from '@angular/core';
import { AdminApi, Configuration } from '../swagger-gen';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private adminApi: AdminApi) {
    this.adminApi.apiAdminBotsGet().subscribe(r => {
      console.log(r);
    });
  }
  title = 'app';
}
