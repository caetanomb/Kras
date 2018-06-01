import { Component, OnInit } from '@angular/core';

import {FlightService} from '../flights/shared/flight.service'
import { Global } from '../shared/GlobalConf';
import {Flight} from '../flights/shared/flight.model'

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css']
})
export class ReportComponent implements OnInit {

  constructor(private flightService : FlightService) {    
   }

  ngOnInit() {
    this.flightService.getFlights(Global.BASE_USER_ENDPOINT + "Flight")
    .subscribe(data => {
      this.flightService.flightList = data;        
    })
  }

}
