import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { AdminApi, BASE_PATH, Configuration } from '../swagger-gen';
import { environment } from '../environments/environment';
import { AuthConfiguration } from './AuthConfiguration';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpModule
  ],
  providers: [
    AdminApi,
    { provide: BASE_PATH, useValue: environment.apiHost },
    { provide: Configuration, useClass: AuthConfiguration }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
