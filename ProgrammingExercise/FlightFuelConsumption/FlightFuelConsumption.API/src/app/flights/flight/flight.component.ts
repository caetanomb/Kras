import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

import { FlightService } from '../shared/flight.service'
import { AirportService } from '../../shared/airport.service'
import { Global } from '../../shared/GlobalConf';
import { Airport } from '../../shared/Airport';

@Component({
  selector: 'app-flight',
  templateUrl: './flight.component.html',
  styleUrls: ['./flight.component.css']
})
export class FlightComponent implements OnInit {  

  airportList : Airport[];
  selectedAirport : number;

  constructor(private flightService : FlightService, private toastr : ToastrService,
    private airportService : AirportService) {

   }

  ngOnInit() {
    this.resetForm();
    this.loadAirports();
  }

  resetForm(form? : NgForm){
    if (form != null)
      form.reset();

    this.flightService.selectedFlight = {
      Id : null,
      DepartureAirportId : null,
      DepartureAirportName : '',
      DestinationAirportId : null,
      DestinationAirportName : '',
      FlightTime : null,
      TakeoffEffort : null
    };
  }  

  onSubmitForm(form? : NgForm){
    debugger;
    if (form.value.Id == null)
    {
      this.flightService.postFlight(Global.BASE_USER_ENDPOINT + "Flight", form.value)
        .subscribe(data => {      
          this.resetForm(form);
          this.flightService.getFlightsNoReturn(Global.BASE_USER_ENDPOINT + "Flight");    
          this.toastr.success('Flight Registered Successfully', "Flight registration");
          })
    }
    else
    {    
      this.flightService.putFlight(Global.BASE_USER_ENDPOINT + "Flight", form.value.Id, form.value)
      .subscribe(data => {        
        this.resetForm(form);
        this.flightService.getFlightsNoReturn(Global.BASE_USER_ENDPOINT + "Flight");        
        this.toastr.info('Flight Updated Successfully', "Flight update");
      })
    }      
  }  

  loadAirports(){
    this.airportService.getAirports(Global.BASE_USER_ENDPOINT + "Airport")
      .subscribe(data => {
        this.airportList = data;
      })
  }
}
