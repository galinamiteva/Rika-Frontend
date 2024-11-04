document.addEventListener("DOMContentLoaded", function () {
    onboardingFunction();
});

function onboardingFunction() {
    const slides = document.querySelectorAll('.container .content');
    let currentSlideIndex = 0;


    slides[currentSlideIndex].style.display = 'block';

    document.querySelectorAll('#onboard-btn').forEach(button => {
        button.addEventListener('click', () => {

            slides[currentSlideIndex].style.display = 'none';


            currentSlideIndex++;


            if (currentSlideIndex < slides.length) {
                slides[currentSlideIndex].style.display = 'block';
            } else {

                window.location.href = "https://localhost:7259/SignUp/SignUpView";
            }
        });
    });
}