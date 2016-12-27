import { Component, ViewChild } from '@angular/core';
import { Nav, Platform } from 'ionic-angular';
import { StatusBar, Splashscreen } from 'ionic-native';
import { Events } from 'ionic-angular';

import { Home } from '../pages/home/home';
import { SearchPage } from '../pages/search/search';
import { LoginPage } from '../pages/login/login';
import { LogoutPage } from '../pages/logout/logout';
import { ProfilePage } from '../pages/profile/profile';


@Component({
  templateUrl: 'app.html'
})
export class MyApp {
  @ViewChild(Nav) nav: Nav;

  rootPage: any = Home;

  pages: Array<{title: string, component: any}>;

  constructor(public platform: Platform, public events: Events) {
    this.initializeApp();

    // used for an example of ngFor and navigation
    this.pages = [
      { title: 'Home', component: Home },
      { title: 'Search a room', component: SearchPage },
      { title: 'Login', component: LoginPage },
    ];

    events.subscribe('user:created', (user, time) => {
      console.log('Welcome', user[0], 'at', time);
      if (user[0] != null) {
        this.pages = [
          { title: 'Home', component: Home },
          { title: 'Search a room', component: SearchPage },
          { title: 'Logout', component: LogoutPage },
          { title: 'Profile', component: ProfilePage }
        ]
      } else {
        this.pages = [
          { title: 'Home', component: Home },
          { title: 'Search a room', component: SearchPage },
          { title: 'Login', component: LoginPage },
        ]
      }
    });


  }

  initializeApp() {
    localStorage.clear();
    this.platform.ready().then(() => {
      // Okay, so the platform is ready and our plugins are available.
      // Here you can do any higher level native things you might need.
      StatusBar.styleDefault();
      Splashscreen.hide();
    });
  }

  openPage(page) {
    // Reset the content nav to have just this page
    // we wouldn't want the back button to show in this scenario
    this.nav.setRoot(page.component, );
  }
}
