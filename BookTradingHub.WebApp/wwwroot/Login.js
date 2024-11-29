document.addEventListener("DOMContentLoaded", function () {
  const container = document.getElementById("container");
  const signInButton = document.querySelector(".toggle-button.sign-in");
  const signUpButton = document.querySelector(".toggle-button.sign-up");

  if (signInButton && signUpButton) {
    // Attach click events to toggle buttons
    signInButton.addEventListener("click", toggleToSignIn);
    signUpButton.addEventListener("click", toggleToSignUp);
  }

  function toggleToSignIn() {
    if (container) {
      container.classList.remove("active"); // Remove 'active' for sign-in
      // Add transition effect
      container.classList.add("transitioning");
      
      setTimeout(() => {
        container.classList.remove("transitioning"); // End transition state after 600ms
      }, 600); // Duration of the animation
    }
  }

  function toggleToSignUp() {
    if (container) {
      container.classList.add("active"); // Add 'active' for sign-up
      // Add transition effect
      container.classList.add("transitioning");
      
      setTimeout(() => {
        container.classList.remove("transitioning"); // End transition state after 600ms
      }, 600); // Duration of the animation
    }
  }
});

// Blazor interop functions
window.toggleToSignIn = function () {
  const container = document.getElementById("container");
  if (container) {
    container.classList.remove("active");
    container.classList.add("transitioning");
    
    setTimeout(() => {
      container.classList.remove("transitioning");
    }, 600);
  }
};

window.toggleToSignUp = function () {
  const container = document.getElementById("container");
  if (container) {
    container.classList.add("active");
    container.classList.add("transitioning");
    
    setTimeout(() => {
      container.classList.remove("transitioning");
    }, 600);
  }

  window.updateAccountState = function (isSignUpActive) {
    const container = document.getElementById("container");
    
    if (container) {
      if (isSignUpActive) {
        container.classList.add("active");
      } else {
        container.classList.remove("active");
      }
    }
  };
  
  window.initializeAccountToggle = function () {
    const container = document.getElementById("container");
    const signInButton = document.querySelector(".toggle-button.sign-in");
    const signUpButton = document.querySelector(".toggle-button.sign-up");
  
    if (signInButton && signUpButton) {
      // Attach click events to toggle buttons
      signInButton.addEventListener("click", () => updateAccountState(false));
      signUpButton.addEventListener("click", () => updateAccountState(true));
    }
  };
  
};
