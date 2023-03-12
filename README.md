# Log-a-Tron (Logatron)
Automated and easy-to-use amateur radio logging software.

# Status
![CI](https://github.com/github/docs/actions/workflows/ci.yml/badge.svg)

# Context

## Requirements
The creation of this software originated from the frustration over existing solutions that were complicated to use, lacked a good user experience, or had flaws in them. At the moment, Logatron is developed to fit my specific needs, which are:
- Automatic logging of a QSO made in WSJT-X
- Manual log input
- Automatic log up- and download to and from LotW
- Connection to OmniRig
- Automatic retrieval of QSO partner data from online sources

## Where does the name come from?
The name for this software was chosen from a selection provided by ChatGPT. The initial prompt was:
> I'm currently developing an amateur radio software. It is supposed to be an easy and simple logging software, but I may also extend it to other use cases in the future, like digital modes, radio control etc. I'm looking for a name that has some reference to the use cases, but is also supposed to be funny. Can you give me a few ideas?

In ChatGPT's own words:
> The name "Log-a-Tron" is a play on words, combining "log" (as in a radio logbook) with "-a-Tron" (as in a fictional robotic or electronic device). The "-a-Tron" suffix has been used in various fictional contexts to give a sense of advanced or futuristic technology. For example, there is a character in the Transformers toy line and animated series named Megatron, and in the Marvel Comics universe, there is a character named Iron Man who has a suit of armor called the Hulkbuster Iron Man Armor.
>
> In the context of an amateur radio software, the name "Log-a-Tron" suggests a high-tech, automated, and efficient logging tool for radio operators.

# TODO
- Remove dependency from OmniRig executable for COM interop
- Add CI
