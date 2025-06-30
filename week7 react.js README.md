# --------------- handout 9 ------------------
###  `ListofPlayers.js`

```javascript
import React from 'react';

const ListofPlayers = () => {
  const players = [
    { name: 'Virat', score: 80 },
    { name: 'Rohit', score: 65 },
    { name: 'Rahul', score: 72 },
    { name: 'Hardik', score: 90 },
    { name: 'Shreyas', score: 45 },
    { name: 'Pant', score: 50 },
    { name: 'Ashwin', score: 78 },
    { name: 'Bumrah', score: 30 },
    { name: 'Shami', score: 85 },
    { name: 'Siraj', score: 40 },
    { name: 'Kuldeep', score: 60 }
  ];

  const filteredPlayers = players.filter(player => player.score >= 70);

  return (
    <div>
      <h3>All Players</h3>
      {players.map((player, index) => (
        <p key={index}>{player.name} - {player.score}</p>
      ))}

      <h3>Players with score 70 or above</h3>
      {filteredPlayers.map((player, index) => (
        <p key={index}>{player.name} - {player.score}</p>
      ))}
    </div>
  );
};

export default ListofPlayers;
```

---

### `IndianPlayers.js`

```javascript
import React from 'react';

const IndianPlayers = () => {
  const oddTeam = ['Virat', 'Rohit', 'Rahul', 'Hardik', 'Pant'];
  const evenTeam = ['Ashwin', 'Shami', 'Bumrah', 'Siraj', 'Kuldeep', 'Shreyas'];

  const [odd1, odd2, ...restOdd] = oddTeam;
  const [even1, even2, ...restEven] = evenTeam;

  const T20Players = ['Kohli', 'Rohit', 'Suryakumar'];
  const RanjiTrophyPlayers = ['Pujara', 'Rahane', 'Shaw'];

  const allPlayers = [...T20Players, ...RanjiTrophyPlayers];

  return (
    <div>
      <h3>Odd Team Players</h3>
      <p>{odd1}, {odd2}, {restOdd.join(', ')}</p>

      <h3>Even Team Players</h3>
      <p>{even1}, {even2}, {restEven.join(', ')}</p>

      <h3>Merged T20 and Ranji Trophy Players</h3>
      <p>{allPlayers.join(', ')}</p>
    </div>
  );
};

export default IndianPlayers;
```

---

### `App.js`

```javascript
import React from 'react';
import ListofPlayers from './ListofPlayers';
import IndianPlayers from './IndianPlayers';

function App() {
  const flag = true;

  return (
    <div className="App">
      {flag ? <ListofPlayers /> : <IndianPlayers />}
    </div>
  );
}

export default App;
```

# --------------- handout 10 --------------

### `App.js`

```javascript
import React from 'react';

const office = {
  name: "Prestige Tech Park",
  rent: 55000,
  address: "Outer Ring Road, Bangalore",
  image: "https://via.placeholder.com/300x200"
};

const officeList = [
  { name: "RMZ Infinity", rent: 65000, address: "Old Madras Road, Bangalore" },
  { name: "Bagmane Tech Park", rent: 60000, address: "CV Raman Nagar, Bangalore" },
  { name: "WeWork", rent: 50000, address: "HSR Layout, Bangalore" }
];

function App() {
  return (
    <div style={{ padding: "20px", fontFamily: "Arial" }}>
      <h1>Office Space Rental</h1>

      <img src={office.image} alt="Office Space" width="300" height="200" />

      <h2>{office.name}</h2>
      <p>{office.address}</p>
      <p style={{ color: office.rent < 60000 ? "red" : "green" }}>
        Rent: ₹{office.rent}
      </p>

      <hr />

      <h2>Other Available Offices</h2>
      {officeList.map((item, index) => (
        <div key={index} style={{ marginBottom: "20px" }}>
          <h3>{item.name}</h3>
          <p>{item.address}</p>
          <p style={{ color: item.rent < 60000 ? "red" : "green" }}>
            Rent: ₹{item.rent}
          </p>
        </div>
      ))}
    </div>
  );
}

export default App;
```
# ------------- handout 11 ---------------

Here is the full answer to **"11. ReactJS-HOL.docx"**, with **code only** as requested:

---

### ✅ `App.js`

```javascript
import React, { Component } from 'react';
import CurrencyConvertor from './CurrencyConvertor';

class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
      counter: 0
    };
  }

  incrementCounter = () => {
    this.setState(prevState => ({ counter: prevState.counter + 1 }));
    this.sayHello();
  };

  decrementCounter = () => {
    this.setState(prevState => ({ counter: prevState.counter - 1 }));
  };

  sayHello = () => {
    console.log("Hello! Static message from sayHello()");
  };

  sayWelcome = (message) => {
    alert(message);
  };

  handleSyntheticEvent = (e) => {
    alert("I was clicked");
  };

  render() {
    return (
      <div style={{ padding: '20px' }}>
        <h2>Counter: {this.state.counter}</h2>
        <button onClick={this.incrementCounter}>Increase</button>
        <button onClick={this.decrementCounter}>Decrease</button>

        <br /><br />

        <button onClick={() => this.sayWelcome("Welcome")}>Say Welcome</button>

        <br /><br />

        <button onClick={this.handleSyntheticEvent}>OnPress</button>

        <br /><br />

        <CurrencyConvertor />
      </div>
    );
  }
}

export default App;
```

### ✅ `CurrencyConvertor.js`

```javascript
import React, { Component } from 'react';

class CurrencyConvertor extends Component {
  constructor(props) {
    super(props);
    this.state = {
      rupees: '',
      euro: ''
    };
  }

  handleChange = (e) => {
    this.setState({ rupees: e.target.value });
  };

  handleSubmit = (e) => {
    e.preventDefault();
    const rupees = parseFloat(this.state.rupees);
    const euroValue = rupees / 90;
    this.setState({ euro: euroValue.toFixed(2) });
  };

  render() {
    return (
      <div>
        <h3>Currency Converter (INR to EUR)</h3>
        <form onSubmit={this.handleSubmit}>
          <input
            type="text"
            value={this.state.rupees}
            onChange={this.handleChange}
            placeholder="Enter amount in INR"
          />
          <button type="submit">Convert</button>
        </form>
        <p>Euro: €{this.state.euro}</p>
      </div>
    );
  }
}

export default CurrencyConvertor;
```

# --------------- handout 12 ----------------


### `Guest.js`

```javascript
import React from 'react';

function Guest() {
  return (
    <div>
      <h2>Welcome Guest</h2>
      <p>Browse available flights and details.</p>
    </div>
  );
}

export default Guest;
```


### `User.js`

```javascript
import React from 'react';

function User() {
  return (
    <div>
      <h2>Welcome User</h2>
      <p>You can now book your tickets.</p>
    </div>
  );
}

export default User;
```

### `App.js`

```javascript
import React, { Component } from 'react';
import Guest from './Guest';
import User from './User';

class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isLoggedIn: false
    };
  }

  handleLogin = () => {
    this.setState({ isLoggedIn: true });
  };

  handleLogout = () => {
    this.setState({ isLoggedIn: false });
  };

  render() {
    let content;
    if (this.state.isLoggedIn) {
      content = <User />;
    } else {
      content = <Guest />;
    }

    return (
      <div style={{ padding: '20px' }}>
        <h1>Ticket Booking Application</h1>
        {this.state.isLoggedIn ? (
          <button onClick={this.handleLogout}>Logout</button>
        ) : (
          <button onClick={this.handleLogin}>Login</button>
        )}
        <hr />
        {content}
      </div>
    );
  }
}

export default App;
```

# ------------- handout 13 --------------

### `BookDetails.js`

```javascript
import React from 'react';

function BookDetails() {
  return (
    <div>
      <h2>Book Details</h2>
      <p>Book Title: Learn React</p>
      <p>Author: John Doe</p>
    </div>
  );
}

export default BookDetails;
```

---

### `BlogDetails.js`

```javascript
import React from 'react';

function BlogDetails() {
  return (
    <div>
      <h2>Blog Details</h2>
      <p>Title: React Hooks Explained</p>
      <p>Author: Jane Smith</p>
    </div>
  );
}

export default BlogDetails;
```

---

### `CourseDetails.js`

```javascript
import React from 'react';

function CourseDetails() {
  return (
    <div>
      <h2>Course Details</h2>
      <p>Course: React for Beginners</p>
      <p>Duration: 6 Weeks</p>
    </div>
  );
}

export default CourseDetails;
```

---

### `App.js`

```javascript
import React, { Component } from 'react';
import BookDetails from './BookDetails';
import BlogDetails from './BlogDetails';
import CourseDetails from './CourseDetails';

class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
      selected: 'book'
    };
  }

  render() {
    let content;

    // Conditional rendering using if-else
    if (this.state.selected === 'book') {
      content = <BookDetails />;
    } else if (this.state.selected === 'blog') {
      content = <BlogDetails />;
    } else {
      content = <CourseDetails />;
    }

    return (
      <div style={{ padding: '20px' }}>
        <h1>Blogger App</h1>

        <button onClick={() => this.setState({ selected: 'book' })}>Book</button>
        <button onClick={() => this.setState({ selected: 'blog' })}>Blog</button>
        <button onClick={() => this.setState({ selected: 'course' })}>Course</button>

        <hr />

        {/* Conditional rendering using variable */}
        {content}

        {/* Conditional rendering using ternary */}
        {this.state.selected === 'book' ? <p>You selected Book</p> :
         this.state.selected === 'blog' ? <p>You selected Blog</p> :
         <p>You selected Course</p>}
      </div>
    );
  }
}

export default App;
```
