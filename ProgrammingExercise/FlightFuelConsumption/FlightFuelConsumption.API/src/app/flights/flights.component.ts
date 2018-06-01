import { Component, OnInit } from '@angular/core';

import {FlightService} from './shared/flight.service'

@Component({
  selector: 'app-flights',
  templateUrl: './flights.component.html',
  styleUrls: ['./flights.component.css'],
  providers:[FlightService]
})
export class FlightsComponent implements OnInit {

  constructor(private flightService : FlightService) {

   }

  ngOnInit() {
  }

}
