import { NgModule, ErrorHandler } from '@angular/core';
import { IonicApp, IonicModule, IonicErrorHandler } from 'ionic-angular';
import { MyApp } from './app.component';
import { Home } from '../pages/home/home';
import { SearchPage } from '../pages/search/search';
import { CountryPage } from '../pages/country/country';
import { RegionPage } from '../pages/region/region';
import { CityPage } from '../pages/city/city';

@NgModule({
  declarations: [
    MyApp,
    Home,
    SearchPage,
    CountryPage,
    RegionPage,
    CityPage
  ],
  imports: [
    IonicModule.forRoot(MyApp)
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    Home,
    SearchPage,
    CountryPage,
    RegionPage,
    CityPage
  ],
  providers: [{provide: ErrorHandler, useClass: IonicErrorHandler}]
})
export class AppModule {
}