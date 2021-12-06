import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { AlpacaListComponent } from './alpaca-list/alpaca-list.component';
import { AlpacaEditComponent } from './alpaca-edit/alpaca-edit.component';
import { AlpacaService } from './services/alpaca.service';
import { HttpHelpersService } from "./services/http-helpers.service";

@NgModule({
  declarations: [
    AppComponent,
    AlpacaListComponent,
    AlpacaEditComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: AlpacaListComponent },
      { path: 'listalpaca', component: AlpacaListComponent },
      { path: 'addalpaca', component: AlpacaEditComponent }
    ])
  ],
  providers: [
      HttpHelpersService,
      AlpacaService
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
