import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';

// call country service
import { CountryService } from '../../providers/country-service';

@Component({
  selector: 'page-search',
  templateUrl: 'search.html',
  providers: [CountryService]
})
export class SearchPage {
  public countries: any[];
  searchQuery: string = '';

  constructor(
    public navCtrl: NavController, 
    public CountryService: CountryService,
  ) {
    this.navCtrl = navCtrl;
    this.loadCountries();
  }
  
  loadCountries(){
    this.CountryService.load()
    .then(data => {
      this.countries = data;
    });
  }

  getItems(ev: any) {
    // Reset items back to all of the items
    this.loadCountries();

    // set val to the value of the searchbar
    let val = ev.target.value;

    // if the value is an empty string don't filter the items
    if (val && val.trim() != '') {
      this.countries = this.countries.filter((country) => {
        return (country.name.toLowerCase().indexOf(val.toLowerCase()) > -1);
      })
    }
  }

}
