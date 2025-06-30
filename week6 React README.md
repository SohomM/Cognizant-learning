#### **Define SPA and its benefits**

**SPA (Single Page Application)** is a web application that loads a single HTML page and dynamically updates content without refreshing the page.
**Benefits**:

* Faster navigation
* Better user experience
* Reduced server load
* Efficient development with frontend frameworks like React


#### **Define React and identify its working**

**React** is a JavaScript library developed by Facebook for building user interfaces, especially for SPAs.
It works by using a virtual DOM to render UI components efficiently based on changes in application state.


#### **Identify the differences between SPA and MPA**

| SPA (Single Page App)           | MPA (Multi Page App)                           |
| ------------------------------- | ---------------------------------------------- |
| Loads once, updates dynamically | Loads a new page for each request              |
| Faster client-side routing      | Slower due to full-page reloads                |
| Uses JavaScript frameworks      | Traditional web apps use server-side rendering |
| Suitable for modern apps        | Suitable for content-heavy or SEO apps         |


#### **Explain Pros & Cons of Single-Page Application**

**Pros**:

* Fast navigation
* Seamless user experience
* Reduced server calls

**Cons**:

* Initial load can be slow
* SEO challenges
* Requires JavaScript to be enabled



**Explain about React** : React is a component-based JavaScript library for building dynamic UIs. It allows developers to break UI into reusable pieces and manage state effectively using hooks or lifecycle methods.

**Define Virtual DOM** : The **Virtual DOM** is a lightweight JavaScript representation of the actual DOM. React compares changes in the virtual DOM and efficiently updates only the changed parts in the real DOM.

#### **Explain Features of React**

* **Component-Based Architecture**
* **Virtual DOM**
* **JSX Syntax**
* **Unidirectional Data Flow**
* **Hooks for State and Lifecycle Management**
* **Efficient Rendering and Reusability**


✅ **Practical / Lab Steps**

 **2. Install Create React App:**

```bash
npm install -g create-react-app
```

 **3. Create a new React app named “myfirstreact”:**

```bash
npx create-react-app myfirstreact
```

 **4. Navigate into the project folder:**

```bash
cd myfirstreact
```

 **6-8. Replace `App.js` content with:**

```javascript
function App() {
  return (
    <div className="App">
      <h1>welcome to the first session of React</h1>
    </div>
  );
}

export default App;
```

 **9. Run the React application:**

```bash
npm start
```

**10. Open browser and type:**

```
http://localhost:3000
```

You should see the message:
**"welcome to the first session of React"** as a heading on the page.

# ---------------------------------------------------------------



#### 1. **Explain React components**

React components are independent, reusable pieces of UI defined in JavaScript that return HTML (via JSX).


#### 2. **Identify the differences between components and JavaScript functions**

* React components return JSX and are used in rendering UI.
* JavaScript functions perform computations or operations and return values (not JSX).


#### 3. **Identify the types of components**

* Class Components
* Function Components


#### 4. **Explain class component**

A class component is a React component defined using ES6 class syntax. It includes lifecycle methods and must extend `React.Component`.


#### 5. **Explain function component**

A function component is a JavaScript function that returns JSX. It can use React Hooks for state and lifecycle.


#### 6. **Define component constructor**

The constructor in a class component is used to initialize state and bind methods. It’s called once when the component is created.


#### 7. **Define render() function**

The `render()` function in class components returns the JSX to be displayed on the screen. It is required in every class component.


### ✅ **Hands-On Instructions & Code**

#### 1. **Create a React project named `StudentApp`:**

```bash
npx create-react-app StudentApp
```


#### 2. **Creating a folder under `src` named `Components`**

#### 3. **Creating a file `Home.js` inside `Components` folder**

**Home.js**

```javascript
import React from 'react';

class Home extends React.Component {
  render() {
    return (
      <div>
        <h2>Welcome to the Home page of Student Management Portal</h2>
      </div>
    );
  }
}

export default Home;
```


#### 4. **Create `About.js` under `src/Components`**

**About.js**

```javascript
import React from 'react';

class About extends React.Component {
  render() {
    return (
      <div>
        <h2>Welcome to the About page of the Student Management Portal</h2>
      </div>
    );
  }
}

export default About;
```


#### 5. **Create `Contact.js` under `src/Components`**

**Contact.js**

```javascript
import React from 'react';

class Contact extends React.Component {
  render() {
    return (
      <div>
        <h2>Welcome to the Contact page of the Student Management Portal</h2>
      </div>
    );
  }
}

export default Contact;
```


#### 6. **Edit `App.js` to invoke all three components**

**App.js**

```javascript
import React from 'react';
import Home from './Components/Home';
import About from './Components/About';
import Contact from './Components/Contact';

function App() {
  return (
    <div className="App">
      <Home />
      <About />
      <Contact />
    </div>
  );
}

export default App;
```


#### 7. **Navigate into StudentApp folder and run the app:**

```bash
cd StudentApp
npm start
```


#### 8. **Open browser and type:**

```
http://localhost:3000
```

# -----------------------------------------------------

###  `Post.js`

```javascript
import React from 'react';

class Post extends React.Component {
  render() {
    return (
      <div>
        <h3>{this.props.title}</h3>
        <p>{this.props.body}</p>
      </div>
    );
  }
}

export default Post;
```

---

### `Posts.js`

```javascript
import React from 'react';
import Post from './Post';

class Posts extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      posts: [],
      hasError: false
    };
  }

  loadPosts() {
    fetch('https://jsonplaceholder.typicode.com/posts')
      .then(response => response.json())
      .then(data => {
        this.setState({ posts: data });
      })
      .catch(error => {
        this.setState({ hasError: true });
        console.error('Error fetching posts:', error);
      });
  }

  componentDidMount() {
    this.loadPosts();
  }

  componentDidCatch(error, info) {
    alert('An error occurred in Posts component.');
    console.error('Error caught in componentDidCatch:', error, info);
  }

  render() {
    return (
      <div>
        <h2>Blog Posts</h2>
        {this.state.posts.map(post => (
          <Post key={post.id} title={post.title} body={post.body} />
        ))}
      </div>
    );
  }
}

export default Posts;
```

###  `App.js`

```javascript
import React from 'react';
import Posts from './Posts';

function App() {
  return (
    <div className="App">
      <Posts />
    </div>
  );
}

export default App;
```

# -----------------------------------------------

### `CohortDetails.module.css`

```css
.box {
  width: 300px;
  display: inline-block;
  margin: 10px;
  padding: 10px 20px;
  border: 1px solid black;
  border-radius: 10px;
}

dt {
  font-weight: 500;
}
```

###  `CohortDetails.js`

```javascript
import React from 'react';
import styles from './CohortDetails.module.css';

function CohortDetails({ name, status, startDate, endDate }) {
  const headingStyle = {
    color: status === 'ongoing' ? 'green' : 'blue'
  };

  return (
    <div className={styles.box}>
      <h3 style={headingStyle}>{name}</h3>
      <dl>
        <dt>Status:</dt>
        <dd>{status}</dd>
        <dt>Start Date:</dt>
        <dd>{startDate}</dd>
        <dt>End Date:</dt>
        <dd>{endDate}</dd>
      </dl>
    </div>
  );
}

export default CohortDetails;
```

