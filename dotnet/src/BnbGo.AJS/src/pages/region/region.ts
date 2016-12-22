import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';

// call country service
import { CityService } from '../../providers/city-service';

import { CityPage } from '../city/city'

@Component({
  selector: 'page-region',
  templateUrl: 'region.html',
  providers: [CityService]
})
export class RegionPage {
  private posts: any;
  private items: any;
  private cityId: string;
  private cityName: string;
  private region: string;

  constructor(public navCtrl: NavController, public navParams: NavParams, public CityService: CityService) {
    this.navCtrl = navCtrl;
    this.cityId = this.navParams.get("regionId");
    this.region = this.navParams.get("regionName");
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

  pushCity(id, name) {
    this.cityId = id;
    this.cityName = name;
    this.navCtrl.push(CityPage , {cityId: this.cityId, cityName: this.cityName});
  }
}
