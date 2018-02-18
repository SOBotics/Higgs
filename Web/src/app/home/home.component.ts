import { Component, OnInit, Inject } from '@angular/core';
import { BASE_PATH, AdminApi } from '../../swagger-gen';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  private loginUrl: string;
  constructor(private adminApi: AdminApi) {
    this.adminApi.apiAdminBotsGet().subscribe(r => {
      console.log(r);
    });
    this.loginUrl = `${environment.apiHost}/Authentication/Login?redirect_uri=${environment.webHost}%2Foauth&scope=all`;
  }

  ngOnInit() {
  }

}
