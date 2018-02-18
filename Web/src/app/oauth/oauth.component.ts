import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Configuration } from '../../swagger-gen';

@Component({
  selector: 'app-oauth',
  templateUrl: './oauth.component.html',
  styleUrls: ['./oauth.component.css']
})
export class OAuthComponent implements OnInit {

  constructor(private authConfiguration: Configuration, private router: Router, private activatedRoute: ActivatedRoute) {

  }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      const accessToken = params['access_token'];
      if (accessToken) {
        localStorage.setItem('access_token', accessToken);
        this.router.navigateByUrl('/');
      }
    });
  }
}
