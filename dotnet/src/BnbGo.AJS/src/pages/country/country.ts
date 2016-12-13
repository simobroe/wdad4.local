import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
// call country service
import { RegionService } from '../../providers/region-service';

import { RegionPage } from '../region/region'

@Component({
  selector: 'page-country',
  templateUrl: 'country.html',
  providers: [RegionService]
})
export class CountryPage {
  private posts: any;
  private countryId: string;
  private items: any;
  private regionId: string;
  private regionName: string;
  private country: string;

  constructor(public navCtrl: NavController, public navParams: NavParams, public RegionService: RegionService) {
    this.countryId = this.navParams.get("countryId");
    this.country = this.navParams.get("countryName");
    this.loadRegions(this.countryId);
  }
  
  loadRegions(countryId){
    this.RegionService.load(countryId)
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

  pushRegion(id, name) {
    this.regionId = id;
    this.regionName = name;
    this.navCtrl.push(RegionPage , {regionId: this.regionId, regionName: this.regionName});
  }
}
