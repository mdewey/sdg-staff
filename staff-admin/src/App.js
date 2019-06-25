import React, { Component } from 'react'
import {
  ListGroup,
  ListGroupItem,
  ListGroupItemHeading,
  ListGroupItemText
} from 'reactstrap'
const API = 'https://sdg-staff-directory-app.herokuapp.com'
export default class Admin extends Component {
  state = {
    companies: [],
    employees: []
  }

  getAllCompanies = () => {
    fetch(`${API}/api/company`)
      .then(resp => resp.json())
      .then(json => {
        this.setState({ companies: json })
      })
  }

  getAllEmployees = () => {
    fetch(`${API}/api/employees`)
      .then(resp => resp.json())
      .then(json => {
        this.setState({ employees: json })
      })
  }

  refresh = () => {
    this.getAllCompanies()
    this.getAllEmployees()
  }

  deleteCompany = company => {
    fetch(API + '/api/company/' + company, { method: 'DELETE' }).then(_ => {
      this.refresh()
    })
  }
  deleteEmployee = (key, id) => {
    fetch(API + `/api/employees/${key}/${id}`, { method: 'DELETE' }).then(_ => {
      this.refresh()
    })
  }

  deleteAllCompany = () => {
    fetch(API + '/api/company', { method: 'DELETE' }).then(_ => {
      this.refresh()
    })
  }

  componentDidMount() {
    this.refresh()
  }

  render() {
    return (
      <div>
        <h1>Hello, world!</h1>
        <section className="all-companies">
          <header>Companies</header>
          <section>
            <p>Total: {this.state.companies.length}</p>
            <button onClick={this.deleteAllCompany}>delete all</button>
          </section>
          <ListGroup>
            {this.state.companies.map(company => {
              return (
                <ListGroupItem key={company.name}>
                  <ListGroupItemHeading>{company.name}</ListGroupItemHeading>
                  <ListGroupItemText>
                    <div>{company.numberOfEmployees} employees</div>
                    <button onClick={() => this.deleteCompany(company.name)}>
                      delete
                    </button>
                  </ListGroupItemText>
                </ListGroupItem>
              )
            })}
          </ListGroup>
        </section>
        <section className="all-employees">
          <header>Employees</header>
          <section>
            <p>Total: {this.state.employees.length}</p>
          </section>
          <ListGroup>
            {this.state.employees.map(emp => {
              return (
                <ListGroupItem>
                  <button
                    onClick={() => this.deleteEmployee(emp.companyKey, emp.id)}
                  >
                    delete
                  </button>
                  <div>{emp.name}</div>
                  <div>{emp.companyKey}</div>
                </ListGroupItem>
              )
            })}
          </ListGroup>
        </section>
      </div>
    )
  }
}
