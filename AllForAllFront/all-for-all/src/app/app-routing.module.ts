import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignupComponent } from './authorization/signup/signup.component';
import { LoginComponent } from './authorization/login/login.component';
import { ProductsSectionComponent } from './products-section/products-section.component';

const routes: Routes = [
  { path: "", redirectTo: "/signup", pathMatch: "full" },
  {
    path: "signup", component: SignupComponent
  },
  {
    path: "login", component: LoginComponent
  },
  {
    path: "categories/:id", component: ProductsSectionComponent
  },
  {
    path: "manufacturers/:id", component: ProductsSectionComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
