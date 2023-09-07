import { HttpClientModule } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit
{
  readonly APIUrl = "http://localhost:7075/api/todoapp/";

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  eInfo: EmpInfo[] = [
    { empFName: "John", empLName: "Doe", dept: "AAP" },
    { empFName: "James", empLName: "Adam", dept: "PMM" }
  ];

  addEmployee()
  {
    this.http.get<EmpInfo[]>(this.APIUrl + 'GetEmployee').subscribe(data => {
        this.eInfo = data;
      }
    );

    alert("added employee");
  }

}

export interface EmpInfo
{
  empFName: string;
  empLName: string;
  dept: string;
}
