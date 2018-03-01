import { Component, OnInit, Inject } from '@angular/core';
import { BASE_PATH, AdminService } from '../../swagger-gen';
import { environment } from '../../environments/environment';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

}
