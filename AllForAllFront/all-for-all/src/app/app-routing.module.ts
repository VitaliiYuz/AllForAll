import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignupComponent } from './authorization/signup/signup.component';
import { LoginComponent } from './authorization/login/login.component';
import { ProductsSectionComponent } from './products-section/products-section.component';
import { HomeComponent } from './home/home.component';
import { ProductComponent } from './product/product.component';

const routes: Routes = [
  { path: "", redirectTo: "/signup", pathMatch: "full" },
  {
    path: "signup", component: SignupComponent
  },
  {
    path: "login", component: LoginComponent
  },
  {
    path: "home", component: HomeComponent
  },
  {
    path: "categories/:categoryId",
    component: ProductsSectionComponent,
  },
  {
    path: "countries/:countryId",
    component: ProductsSectionComponent,
  },
  {
    path: "manufacturers/:manufacturerId",
    component: ProductsSectionComponent,
  },
  {
    path: "products/:productId",
    component: ProductComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
