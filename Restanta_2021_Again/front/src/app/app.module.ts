import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import {HttpClientModule} from "@angular/common/http";
import { MainComponent } from './main/main.component';
import { DestinationComponent } from './destination/destination.component';

@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    DestinationComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
