import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FlightsComponent } from './flights/flights.component';
import { FlightComponent } from './flights/flight/flight.component';
import { FlightListComponent } from './flights/flight-list/flight-list.component';
import { ToastrModule } from 'ngx-toastr';
import { ReportComponent } from './report/report.component';

@NgModule({
  declarations: [
    AppComponent,
    FlightsComponent,
    FlightComponent,
    FlightListComponent,
    ReportComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule, 
    HttpClientModule,
    ToastrModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
