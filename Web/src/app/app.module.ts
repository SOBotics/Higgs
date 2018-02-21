import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { AdminApi, BASE_PATH, Configuration } from '../swagger-gen';
import { environment } from '../environments/environment';
import { AuthConfiguration } from './AuthConfiguration';
import { OAuthComponent } from './oauth/oauth.component';
import { RouterModule } from '@angular/router';
import { appRouts } from './app.routes';
import { HomeComponent } from './home/home.component';
import { AuthService } from './services/auth.service';
import { ReportComponent } from './report/report.component';
import { TimeAgoPipe } from './pipes/MessageTimePipe';


@NgModule({
  declarations: [
    AppComponent,
    OAuthComponent,
    HomeComponent,
    ReportComponent,
    TimeAgoPipe
  ],
  imports: [
    BrowserModule,
    HttpModule,
    RouterModule.forRoot(appRouts)
  ],
  providers: [
    AdminApi,
    { provide: BASE_PATH, useValue: environment.apiHost },
    { provide: Configuration, useClass: AuthConfiguration },
    AuthService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
