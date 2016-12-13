import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';

// call country service
import { CityService } from '../../providers/city-service';

import { Home } from '../home/home'

@Component({
  selector: 'page-region',
  templateUrl: 'region.html',
  providers: [CityService]
})
export class RegionPage {
  private posts: any;
  private regionId: string;
  private items: any;
  private cityId: string;

  constructor(public navCtrl: NavController, public navParams: NavParams, public CityService: CityService) {
    this.navCtrl = navCtrl;
    this.cityId = this.navParams.get("regionId");
    this.loadCities(this.cityId);
  }

  loadCities(regionId){
    this.CityService.load(regionId)
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

  pushCity(id) {
    this.cityId = id;
    this.navCtrl.push(Home , {cityId: this.cityId});
  }
}
