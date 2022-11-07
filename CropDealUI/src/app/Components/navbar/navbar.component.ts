import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  role!: string | null;

  constructor(private router:Router) { }


  ngOnInit(): void {
    this.role = localStorage.getItem('role')
  }

  onLogout(){
   
    localStorage.clear()
    alert('Logged Out')
    this.router.navigate(['/login'])
  }
}
