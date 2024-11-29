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
    }
  }

  function toggleToSignUp() {
    if (container) {
      container.classList.add("active"); // Add 'active' for sign-up
    }
  }
});

// Expose an initialization function to handle Blazor lifecycle integration
window.initializeAccountToggle = function () {
  const container = document.getElementById("container");
  const signInButton = document.querySelector(".toggle-button.sign-in");
  const signUpButton = document.querySelector(".toggle-button.sign-up");

  if (signInButton && signUpButton) {
    // Reinitialize event listeners if DOM has changed
    signInButton.addEventListener("click", toggleToSignIn);
    signUpButton.addEventListener("click", toggleToSignUp);
  }

  function toggleToSignIn() {
    if (container) {
      container.classList.remove("active"); // Remove 'active' for sign-in
    }
  }

  function toggleToSignUp() {
    if (container) {
      container.classList.add("active"); // Add 'active' for sign-up
    }
  }
};