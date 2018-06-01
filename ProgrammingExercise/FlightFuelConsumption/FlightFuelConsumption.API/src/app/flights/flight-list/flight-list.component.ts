import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

import {FlightService} from '../shared/flight.service'
import { Global } from '../../shared/GlobalConf';
import {Flight} from '../shared/flight.model'

@Component({
  selector: 'app-flight-list',
  templateUrl: './flight-list.component.html',
  styleUrls: ['./flight-list.component.css']
})
export class FlightListComponent implements OnInit {

  //flightList : Flight[];  

  constructor(private flightService : FlightService, private toastr : ToastrService) {     
  }

  ngOnInit() {
    this.flightService.getFlights(Global.BASE_USER_ENDPOINT + "Flight")
    .subscribe(data => {
      this.flightService.flightList = data;        
    })    
  }

  showForEdit(fli : Flight){    
    this.flightService.selectedFlight = Object.assign({}, fli);    
  }

  onDelete(id : number){

    if (confirm('Are you sure to delete this flight?') == true){
      this.flightService.deleteFlight(Global.BASE_USER_ENDPOINT + "Flight", id)
        .subscribe(data => {
          this.flightService.getFlightsNoReturn(Global.BASE_USER_ENDPOINT + "Flight");        
          this.toastr.warning('Flight deleted Successfully', "Flight Removal");
        })
    }    
  }

}
