const axios = require('axios')
const faker = require('faker')

const COMPANY_KEY = 'fakebook'

// axios.get(`https://sdg-staff-directory-app.herokuapp.com//api/${COMPANY_KEY}/employee`).then(resp => {
//   console.log({ data: resp.data })
// })

axios.get('https://randomuser.me/api/?results=10').then(resp => {
  console.log({ data: resp.data })
  resp.data.results.forEach(person => {
    const output = {
      firstName: person.name.first,
      lastName: person.name.last,
      birthday: person.dob.date,
      hiredDate: person.registered.date,
      isFullTime: Math.ceil(Math.random() * 100) % 2 === 1,
      profileImage: person.picture.large,
      jobTitle: faker.name.jobTitle(),
      jobDescription: faker.lorem.sentences(),
      phoneNumber: person.cell,
      address: `${person.location.street}`,
      city: person.location.city,
      state: person.location.state,
      zip: person.location.postcode,
      salary: Math.ceil(Math.random() * 150) * 1000,
      gender: person.gender,
      email: person.email,
      emergencyContactPerson: faker.name.findName(),
      emergencyContactPhone: faker.phone.phoneNumber()
    }
    console.log({ output })
    axios
      .post(`http://localhost:5000/api/${COMPANY_KEY}/employees`, output)
      .then(resp => {
        console.log('added', { output })
      })
  })
})
