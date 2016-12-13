import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';

// call country service
import { CityService } from '../../providers/city-service';

import { CityPage } from '../city/city'

@Component({
  selector: 'page-home',
  templateUrl: 'home.html',
  providers: [CityService]
})

export class Home {
  private cityId: string;
  private posts: any;

  constructor(public navCtrl: NavController, public navParams: NavParams, public CityService: CityService) {
    this.navCtrl = navCtrl;
    this.loadCities();
  }

  loadCities(){
    this.CityService.load(0)
    .then(data => {
      this.posts = data;
    });
  }

  getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
  }

  pushCity() {
    this.cityId = this.getRandomInt(1, this.posts.length);
    this.navCtrl.push(CityPage , {cityId: this.cityId});
  }
}
