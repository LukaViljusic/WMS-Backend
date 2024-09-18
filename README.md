# WMS-Backend

WMS Backend is a web-based backend system designed for task management, enabling multiple users to collaborate within shared workspaces. The system allows users to organize and track tasks, manage work items, and collaborate effectively within different spaces. It supports seamless collaboration among team members and integrates with various issue management systems for efficient project management.

## Features

### Spaces:
- Each user can create one or more Spaces.
- To create a new Space, the following must be defined:
  - **Name**
  - **Description**
- Users can invite other users to a Space via email.

### Work Item Types:
- Users can define Work Item Types, representing different types of tasks (e.g., bug, task).
- When creating a new Work Item Type, the following must be defined:
  - **Name**
  - **Color** (to be displayed with all Work Items of this type).

### Work Item States:
- For each Work Item Type, users can define Work Item States, which represent the current status of a Work Item.
- When creating a new Work Item State, the following must be defined:
  - **Name**
  - **Color**
  - Whether it is an initial or final state.

### Work Items:
- Users can add Work Items to a Space, visible to all Space members.
- When creating a new Work Item, the following must be specified:
  - **Name**
  - **Type** (one of the defined Work Item Types)
  - **Responsible person** (one of the Space members)
  - **Initial state** (one of the defined Work Item States)
  - Optional fields: **Description**, **Deadline**, **Time estimate**.

