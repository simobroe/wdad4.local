import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
// call country service
import { CountryService } from '../../providers/country-service';

import { CountryPage } from '../country/country'

@Component({
  selector: 'page-search',
  templateUrl: 'search.html',
  providers: [CountryService]
})
export class SearchPage {
  private posts: any;
  private items: any;
  private countryId: string;
  private countryName: string;

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
      this.posts = data;
      this.initializeItems();
    });
  }

  initializeItems() {
    this.items = this.posts;
  }

  getItems(ev: any) {
    // Reset items back to all of the items
    this.initializeItems();

    // set val to the value of the searchbar
    let val = ev.target.value;

    // if the value is an empty string don't filter the items
    if (val && val.trim() != '') {
      this.items = this.items.filter((item) => {
        return (item.name.toLowerCase().indexOf(val.toLowerCase()) > -1);
      })
    }
  }

  pushCountry(id, name) {
    this.countryId = id;
    this.countryName = name;
    this.navCtrl.push(CountryPage , {countryId: this.countryId, countryName: this.countryName});
  }

}
